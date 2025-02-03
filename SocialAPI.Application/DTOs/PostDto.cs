using System;

namespace SocialAPI.Application.DTOs
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
    }

    public class CreatePostDto
    {
        public string Content { get; set; }
    }

    public class UpdatePostDto
    {
        public string Content { get; set; }
    }
} 