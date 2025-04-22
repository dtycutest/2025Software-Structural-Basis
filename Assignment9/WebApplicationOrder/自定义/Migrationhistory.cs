using System;
using System.Collections.Generic;

namespace WebApplicationOrder.自定义;

public partial class Migrationhistory
{
    public string MigrationId { get; set; } = null!;

    public string ContextKey { get; set; } = null!;

    public byte[] Model { get; set; } = null!;

    public string ProductVersion { get; set; } = null!;
}
