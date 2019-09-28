

using System;

[Serializable]
class GetImage : BaseMessage<GetImage>
{
    private GetImage() : base("get_image") { }

    public static GetImage Create()
    {
        return new GetImage();
    }
}