namespace LU2WebApi.Models
{
    public class Object2D
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public float RotationZ { get; set; }
        public int SortingLayer { get; set; }
        public string PrefabId { get; set; }
        public Guid Environment2DId { get; set; }
        //public Environment2D Environment3DId { get; set; }
    }
}
