

using System;
using Newtonsoft.Json;

[JsonObject]
class GetImage : BaseMessage<GetImage>
{
    private GetImage() : base("get_image") { }

    public static GetImage Create()
    {
        return new GetImage();
    }
}