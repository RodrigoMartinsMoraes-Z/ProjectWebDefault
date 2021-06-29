namespace Project.Domain.Products
{
    public class Image
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string Base64 { get; set; }
        public bool BackGround { get; set; }

        public virtual Product Product { get; set; }
    }
}
