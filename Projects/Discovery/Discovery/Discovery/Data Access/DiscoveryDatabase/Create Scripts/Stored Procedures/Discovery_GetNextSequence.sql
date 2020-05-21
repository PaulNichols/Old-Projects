IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetNextSequence')
	BEGIN
		DROP  Procedure  Discovery_GetNextSequence
	END

GO

CREATE Procedure Discovery_GetNextSequence
      @name nvarchar(255)
AS
      DECLARE @newValue int

      SET NOCOUNT ON

      UPDATE Discovery_Sequence
      SET @newValue = [CurrentValue] = ([CurrentValue] + [Increment])
      WHERE [Name] = @name

      IF @@rowcount = 0 BEGIN
            RETURN -1
      END

      SELECT @newValue as CurrentValue

GO