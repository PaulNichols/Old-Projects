USE [OMAM]
GO
/****** Object:  View [dbo].[vw_LatestOffShoreFundPrices]    Script Date: 02/22/2008 22:22:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_LatestOffShoreFundPrices]
AS
SELECT     TOP (100) PERCENT dbo.OMAMFund.FundName, dbo.OffShoreFundPrices.Price, dbo.OMAMFund.Currency, dbo.OMAMFund.Id AS OMAMFundId
FROM         dbo.OMAMFund LEFT OUTER JOIN
                      dbo.OffShoreFundPrices ON dbo.OffShoreFundPrices.OMAMFundId = dbo.OMAMFund.Id AND dbo.OffShoreFundPrices.CreatedDate =
                          (SELECT     MAX(CreatedDate) AS Expr1
                            FROM          dbo.OffShoreFundPrices
                            WHERE      (OMAMFundId = dbo.OMAMFund.Id))
WHERE     (dbo.OMAMFund.OffShore = 1)
GROUP BY dbo.OMAMFund.FundName, dbo.OffShoreFundPrices.Price, dbo.OffShoreFundPrices.CreatedDate, dbo.OMAMFund.Id, dbo.OMAMFund.Currency
ORDER BY dbo.OMAMFund.FundName

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[25] 4[29] 2[28] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "OMAMFund"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 226
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "OffShoreFundPrices"
            Begin Extent = 
               Top = 6
               Left = 244
               Bottom = 121
               Right = 412
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_LatestOffShoreFundPrices'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_LatestOffShoreFundPrices'