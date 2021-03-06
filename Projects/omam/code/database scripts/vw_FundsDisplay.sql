USE [OMAM]
GO
/****** Object:  View [dbo].[vw_FundsDisplay]    Script Date: 03/03/2008 23:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_FundsDisplay]
AS
SELECT     dbo.OMAMFund.FundName, dbo.OMAM_LNK_FundCategories.FundId, dbo.OMAMFund.FactsheetFile, dbo.OMAMFund.FactsheetURL, 
                      ISNULL(dbo.vw_FundPricesLatest.BidPrice, dbo.OffShoreFundPrices.Price) AS BidPrice, dbo.vw_FundPricesLatest.OfferPrice, '' AS OMAMTV, 
                      dbo.OMAMFund.CityWireRatingURL, dbo.OMAMFund.CityWitreRatingCopy, dbo.OMAMFund.OBSRRatingURL, dbo.OMAMFund.OBSRRatingCopy, 
                      dbo.OMAMFund.SPRatingCopy, dbo.OMAMFund.SPRatingURL, dbo.OMAM_LNK_FundCategories.CategoryId, dbo.vw_PortalCategories.PortalId
FROM         dbo.OMAM_LNK_FundCategories INNER JOIN
                      dbo.OMAMFund ON dbo.OMAMFund.Id = dbo.OMAM_LNK_FundCategories.FundId INNER JOIN
                      dbo.vw_PortalCategories ON dbo.OMAM_LNK_FundCategories.CategoryId = dbo.vw_PortalCategories.Id LEFT OUTER JOIN
                      dbo.OffShoreFundPrices ON dbo.OffShoreFundPrices.OMAMFundId = dbo.OMAMFund.Id AND dbo.OMAMFund.OffShore = 1 LEFT OUTER JOIN
                      dbo.vw_FundPricesLatest ON dbo.OMAMFund.FundCode = dbo.vw_FundPricesLatest.FundCode AND dbo.OMAMFund.OffShore = 0

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[34] 4[28] 2[21] 3) )"
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
         Begin Table = "OMAM_LNK_FundCategories"
            Begin Extent = 
               Top = 38
               Left = 45
               Bottom = 116
               Right = 196
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "OMAMFund"
            Begin Extent = 
               Top = 14
               Left = 220
               Bottom = 158
               Right = 434
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "OffShoreFundPrices"
            Begin Extent = 
               Top = 82
               Left = 490
               Bottom = 230
               Right = 641
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vw_FundPricesLatest"
            Begin Extent = 
               Top = 120
               Left = 38
               Bottom = 228
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "vw_PortalCategories"
            Begin Extent = 
               Top = 6
               Left = 679
               Bottom = 150
               Right = 830
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
      Begin ColumnWidths = 15
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_FundsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_FundsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_FundsDisplay'