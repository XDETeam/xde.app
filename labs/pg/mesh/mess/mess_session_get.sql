CREATE OR REPLACE FUNCTION mesh.session_get()
RETURNS text
AS $$
DECLARE _sid text;
BEGIN
	BEGIN
        SELECT current_setting('mess.sid') INTO _sid;
    EXCEPTION
        WHEN SQLSTATE '42704' THEN
            RETURN NULL;
    END;
    RETURN _sid;
END
$$ LANGUAGE PLPGSQL;
