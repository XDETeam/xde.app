CREATE OR REPLACE FUNCTION mesh.mess_version_guard_trigger()
RETURNS TRIGGER
LANGUAGE PLPGSQL
AS
$$
BEGIN
    IF NEW.version = OLD.version
    THEN
		NEW.version := NEW.version + 1;
        RETURN NEW;
    ELSE
        RAISE EXCEPTION 'Version conflict. You are attempting to update node "%" with version %, but the actual version is %', NEW.url, NEW.version, OLD.version;
    END IF;
END
$$;

CREATE TRIGGER mess_version_guard
    BEFORE UPDATE
    ON mess.mesh2
    FOR EACH ROW
EXECUTE PROCEDURE mesh.mess_version_guard_trigger();
