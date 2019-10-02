

using System;
using Newtonsoft.Json;

[JsonObject]
class GetHeightmap : BaseMessage<GetHeightmap>
{
    private GetHeightmap() : base("get_heightmap") { }

    public static GetHeightmap Create()
    {
        return new GetHeightmap();
    }
}