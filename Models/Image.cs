﻿namespace Models
{
    public class Image
    {

        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] ImageFile { get; set; }
    }
}
