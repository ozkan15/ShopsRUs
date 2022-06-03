namespace ShopsRUs.DTO
{
    public class BillInputModel
    {
        public int UserId { get; set; }
        public List<BillDetailInputModel> BillDetails { get; set; }
    }
}
