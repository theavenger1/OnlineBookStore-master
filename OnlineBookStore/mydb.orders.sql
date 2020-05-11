CREATE TABLE [mydb].[orders] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [order_state] BIT            NOT NULL,
    [userId]      NVARCHAR (128) NOT NULL,
    [OrderDate]   DATETIME       NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [PK_mydb.orders] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_mydb.orders_dbo.AspNetUsers_userId] FOREIGN KEY ([userId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_userId]
    ON [mydb].[orders]([userId] ASC);

