using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyGroup.Data;
using StudyGroup.Models;
using StudyGroup.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudyGroup.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocumentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Document/Create/{id}/{documentId?}")]
        public IActionResult Create(int id, int? documentId)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Id == id);
            if (group != null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var docsGroup = new Group
                {
                    Id = group.Id,
                    Name = group.Name,
                    GroupAdmin = group.GroupAdmin,
                    GroupImage = group.GroupImage,
                };
                if (documentId != 0)
                {
                    var doc = _context.Documents.FirstOrDefault(d => d.Id == documentId);
                    
                    //edit document
                    
                    DocumentViewModel viewModel = new DocumentViewModel
                    {
                        GroupId = group.Id,
                        Document = doc,
                        ErrorMessage = null,
                        Group = docsGroup,
                        CurrentUser = claim.Value
                    };
                    return View(viewModel);
                }
                else
                {
                    DocumentViewModel viewModel = new DocumentViewModel
                    {
                        GroupId = group.Id,
                        ErrorMessage = null,
                        Group = docsGroup,
                        CurrentUser = claim.Value,
                        Document = null
                    };
                    return View(viewModel);
                }
                
            }
            else
            {
                return RedirectToAction("ViewGroup", "Group", new { name = group.Name });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Save(DocumentViewModel CreatedDocument)
        {
            if (CreatedDocument.Document.Id != 0)
            {
                // Document already Exists then it's cool
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var DocumentExact = _context.Documents.FirstOrDefault(d => d.Id == CreatedDocument.Document.Id);
                var group = _context.Groups.FirstOrDefault(g => g.Id == DocumentExact.GroupId);
                var groupname = group.Name;

                if (DocumentExact.SenderId == claim.Value)
                {
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0)
                    {

                        byte[] file = null;

                        using (var fs1 = files[0].OpenReadStream())
                        {
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                file = ms1.ToArray();
                            }
                        }
                        DocumentExact.File = file;
                        DocumentExact.FileType = files[0].FileName.Split(".")[1].ToString();
                    }
                    else
                    {
                        if(DocumentExact.Name == CreatedDocument.Document.Name &&
                        DocumentExact.Description == CreatedDocument.Document.Description)
                        {
                            DocumentViewModel viewModel = new DocumentViewModel
                            {
                                Document = CreatedDocument.Document,
                                GroupId = CreatedDocument.GroupId,
                                ErrorMessage = "You Have not Made Any Changes",
                                Group = group,
                                CurrentUser = claim.Value
                            };
                            return View(nameof(Create),viewModel);
                        }
                    }
                    DocumentExact.Name = CreatedDocument.Document.Name;
                    DocumentExact.Description = CreatedDocument.Document.Description;
                    await _context.SaveChangesAsync();
                }


                return RedirectToAction("ViewAll", "Document", new { id = group.Id });

            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] file = null;

                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            file = ms1.ToArray();
                        }
                    }

                    var document = CreatedDocument.Document;

                    document.File = file;
                    document.FileType = files[0].FileName.Split(".")[1].ToString();
                    document.DateSent = DateTime.Now;

                    
                    document.SenderId = claim.Value;
                    document.GroupId = CreatedDocument.GroupId;
                    _context.Documents.Add(document);
                    await _context.SaveChangesAsync();

                    var group = _context.Groups.FirstOrDefault(g => g.Id == CreatedDocument.Group.Id);
                    return RedirectToAction("ViewAll", "Document", new { id = group.Id });
                }
                else
                {
                    DocumentViewModel viewModel = new DocumentViewModel
                    {
                        ErrorMessage = "No File was sent",
                        Group = CreatedDocument.Group,
                        CurrentUser = claim.Value,
                        GroupId = CreatedDocument.GroupId
                    };
                    return View(nameof(Create), viewModel);
                }
            }

        }

        public IActionResult ViewAll(int id)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Id == id);
            if (group != null)
            {
                var documents = _context.Documents.Where(d => d.GroupId == group.Id);
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var docGroup = new Group
                {
                    GroupAdmin = group.GroupAdmin,
                    Name = group.Name,
                    Id = group.Id,
                    GroupImage = group.GroupImage
                };

                DocumentDetailsViewModel viewModel = new DocumentDetailsViewModel
                {
                    CurrentUser = claim.Value,
                    Documents = documents,
                    GroupAdmin = group.GroupAdmin,
                    Group = docGroup
                };
                return View(viewModel);
            }
            return RedirectToAction("ViewGroup", "Group", new { name = group.Name });
        }

        [Route("Document/Download/{id}/{group}")]
        public IActionResult Download(int id, int group)
        {
            var groupExact = _context.Groups.FirstOrDefault(g => g.Id == group);
            var documentExact = _context.Documents.FirstOrDefault(d => d.Id == id);
            if (groupExact != null && documentExact != null)
            {
                if(documentExact.GroupId == groupExact.Id)
                {
                    if (documentExact.FileType == "pdf")
                    {
                        return new FileContentResult(documentExact.File, "application/" + documentExact.FileType);
                    }
                    else
                    {
                        string contentType = "application/" + documentExact.FileType;
                        string downloadName = documentExact.Name + "." + documentExact.FileType;
                        return File(documentExact.File, contentType, downloadName);
                    }
                }
                else
                {
                    return RedirectToAction("ViewAll", "Document", new { name = groupExact.Name });
                }
            }
            else
            {
                return RedirectToAction("ViewAll", "Document", new { name = groupExact.Name });
            }
        }

        //[Route("Document/Edit/{id}/{group}")]
        //public IActionResult Edit(int id, int group)
        //{
        //    var groupExact = _context.Groups.FirstOrDefault(g => g.Id == group);
        //    var documentExact = _context.Documents.FirstOrDefault(d => d.Id == id);

        //    if (groupExact != null && documentExact != null)
        //    {
        //        var claimsIdentity = (ClaimsIdentity)User.Identity;
        //        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        //        if(documentExact.GroupId== groupExact.Id && documentExact.SenderId == claim.Value)
        //        {
        //            DocumentViewModel viewModel = new DocumentViewModel
        //            {
        //                GroupId = groupExact.Id,
        //                Document = documentExact
        //            };
        //            return RedirectToAction(nameof(Create));
        //        }
        //        else
        //        {
        //            return RedirectToAction("ViewAll", "Document", new { name = groupExact.Name });
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("ViewAll", "Document", new { name = groupExact.Name });
        //    }
        //}

        //public IActionResult Edit(Document document)
        //{
        //    var documentExact = _context.Documents.FirstOrDefault(d => d.Id == document.Id);
        //    var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //    if (documentExact != null && documentExact.SenderId == claim.Value)
        //    {
        //        documentExact.Name = document.Name;
        //        documentExact.Description = document.Description;
        //        documentExact.File
        //    }
        //    else
        //    {
        //        return RedirectToAction("ViewAll", "Document", new { name = groupExact.Name });
        //    }
        //}

        [Route("Document/Delete/{id}/{group}")]
        public async Task<IActionResult> Delete(int id, int group)
        {
            var groupExact = _context.Groups.FirstOrDefault(g => g.Id == group);
            var documentExact = _context.Documents.FirstOrDefault(d => d.Id == id);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (groupExact != null && documentExact != null && documentExact.SenderId == claim.Value)
            {
                _context.Documents.Remove(documentExact);
                await  _context.SaveChangesAsync();
                return RedirectToAction("ViewAll", "Document", new { id = groupExact.Id });
            }
            else
            {
                return RedirectToAction("ViewAll", "Document", new { id = groupExact.Id });
            }
        }
    }
}
