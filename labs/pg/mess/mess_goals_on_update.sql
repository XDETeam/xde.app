CREATE OR REPLACE FUNCTION mesh.mess_goals_on_update()
RETURNS trigger
AS $$
BEGIN
    UPDATE
        mesh.mess
    SET
        content = NEW.content
    WHERE
        id = (NEW.goal)::ltree
    ;
	
	RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER
    on_update
INSTEAD OF UPDATE ON
    mesh.mess_goals
FOR EACH ROW EXECUTE PROCEDURE
    mesh.mess_goals_on_update()
;
