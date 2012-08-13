CREATE TABLE [dbo].[verificationbody](
	[id_verificationbody] [int] IDENTITY(1,1) NOT NULL,
	[del] [bit] NULL CONSTRAINT [DF_verificationbody_del]  DEFAULT ((0)),
	[guid] [nchar](40) NULL CONSTRAINT [DF_verificationbody_guid]  DEFAULT (newid()),
	[id_verification] [int] NULL,
	[order_number] [nchar](15) NULL,
	[order_found] [bit] NULL,
	[order_status_t] [nchar](255) NULL,
	[order_status] [nchar](10) NULL,
	[order_status_fact_t] [nchar](255) NULL,
	[order_status_fact] [nchar](10) NULL,
	[order_in] [datetime] NULL,
	[order_out] [datetime] NULL,
	[order_action_t] [nchar](255) NULL,
	[order_action] [nchar](255) NULL,
	[order_user] [int] NULL,
	[exported] [bit] NULL CONSTRAINT [DF_verificationbody_exported]  DEFAULT ((0))
) ON [PRIMARY]
