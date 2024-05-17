USE [TestMatchProfiler]
GO

INSERT INTO [dbo].[Author] ([Describe]) VALUES ('Author XYZ 1') 
INSERT INTO [dbo].[Author] ([Describe]) VALUES ('Author XYZ 2') 
INSERT INTO [dbo].[Author] ([Describe]) VALUES ('Author XYZ 3') 
INSERT INTO [dbo].[Author] ([Describe]) VALUES ('Author XYZ 4') 
INSERT INTO [dbo].[Author] ([Describe]) VALUES ('Author XYZ 5') 
INSERT INTO [dbo].[Author] ([Describe]) VALUES ('Author XYZ 6') 
GO

INSERT INTO [dbo].[LegalEntity] ([Describe]) VALUES ('LegalEntity XYZ 1')
INSERT INTO [dbo].[LegalEntity] ([Describe]) VALUES ('LegalEntity XYZ 2')
INSERT INTO [dbo].[LegalEntity] ([Describe]) VALUES ('LegalEntity XYZ 3')
INSERT INTO [dbo].[LegalEntity] ([Describe]) VALUES ('LegalEntity XYZ 4')
INSERT INTO [dbo].[LegalEntity] ([Describe]) VALUES ('LegalEntity XYZ 5')
INSERT INTO [dbo].[LegalEntity] ([Describe]) VALUES ('LegalEntity XYZ 6')

GO

INSERT INTO [dbo].[LegalContract] ([Author],[LegalEntity],[DescribeLegalEntity],[CreatedProcess],[UpdatedProcess]) VALUES (1,1,'DescribeLegalEntity teste 1,1',GETDATE(),null)
INSERT INTO [dbo].[LegalContract] ([Author],[LegalEntity],[DescribeLegalEntity],[CreatedProcess],[UpdatedProcess]) VALUES (1,2,'DescribeLegalEntity teste 1,2',GETDATE(),null)
INSERT INTO [dbo].[LegalContract] ([Author],[LegalEntity],[DescribeLegalEntity],[CreatedProcess],[UpdatedProcess]) VALUES (1,3,'DescribeLegalEntity teste 1,3',GETDATE(),null)
INSERT INTO [dbo].[LegalContract] ([Author],[LegalEntity],[DescribeLegalEntity],[CreatedProcess],[UpdatedProcess]) VALUES (2,1,'DescribeLegalEntity teste 2,1',GETDATE(),null)
INSERT INTO [dbo].[LegalContract] ([Author],[LegalEntity],[DescribeLegalEntity],[CreatedProcess],[UpdatedProcess]) VALUES (3,2,'DescribeLegalEntity teste 3,2',GETDATE(),null)
GO