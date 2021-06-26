CREATE OR REPLACE FUNCTION mesh.mess_log_trigger()
RETURNS TRIGGER
LANGUAGE plpgsql
AS
$$
BEGIN
    INSERT INTO mess.log (owner, url, content, version)
    VALUES (mess.session_get(), NEW.url, NEW.content, NEW.version);

    RETURN NEW;
END
$$;

CREATE TRIGGER mess_log
    AFTER UPDATE OR INSERT
    ON mess.mesh2
    FOR EACH ROW
EXECUTE PROCEDURE mesh.mess_log_trigger();
