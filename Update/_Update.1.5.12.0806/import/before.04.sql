CREATE TABLE [dbo].[place](
        [id_place] [int] NULL,
        [name] [nchar](255) NULL,
        [del] [smallint] NULL CONSTRAINT [DF_place_del]  DEFAULT ((0)),
        [server] [nchar](255) NULL,
        [path] [nchar](255) NULL,
        [username] [nchar](255) NULL,
        [password] [nchar](255) NULL
) ON [PRIMARY]

