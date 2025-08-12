CREATE TRIGGER trg_Update_MemberCount
ON ProjectUser
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Projects
    SET MemberCount = (
        SELECT COUNT(*)
        FROM ProjectUser
        WHERE ProjectId = Projects.Id
    )
    WHERE Id IN (
        SELECT ProjectId FROM inserted
        UNION
        SELECT ProjectId FROM deleted
    );
END