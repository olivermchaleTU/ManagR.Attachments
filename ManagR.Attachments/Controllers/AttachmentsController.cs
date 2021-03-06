﻿using System;
using System.Threading.Tasks;
using ManagR.Attachments.Models.ViewModels;
using ManagR.Attachments.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagR.Attachments.Controllers
{
    [Authorize(Policy = "spectator")]
    public class AttachmentsController : Controller
    {
        private IAttachmentsRepository _attachmentsRepository;
        public AttachmentsController(IAttachmentsRepository attachmentsRepository)
        {
            _attachmentsRepository = attachmentsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> PrepareAttachments([FromBody] UploadFilesVm files)
        {
            var preparedAttachments = await _attachmentsRepository.PrepareFileUpload(files);
            return Ok(preparedAttachments);
        }

        [HttpGet]
        public async Task<IActionResult> GetAttachments(Guid id)
        {
            var attachments = await _attachmentsRepository.GetAttachments(id);
            return Ok(attachments);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAttachmentStatus([FromBody] UpdateStatusVm status)
        {
            var success = await _attachmentsRepository.UpdateAttachmentStatus(status);
            if (success)
            {
                return Ok(success);
            }
            return new StatusCodeResult(500);
        }
    }
}