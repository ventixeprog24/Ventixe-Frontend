﻿namespace Authentication.Dtos
{

    //This might really be HeaderPartialViewModel since its a model for the headerPartial...
    public class HeaderUserProfileDto
    {
        public string UserId { get; set; } = null!;
        public string? FullName { get; set; }
    }
}
