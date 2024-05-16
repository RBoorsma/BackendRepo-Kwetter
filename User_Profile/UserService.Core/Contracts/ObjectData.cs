namespace UserService.Core.Contracts;

public class ObjectData<T>
{
    public ActionResult Result { get; set; }
    public T? ResponseBody { get; set; }
}