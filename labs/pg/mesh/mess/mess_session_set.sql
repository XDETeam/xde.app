CREATE OR REPLACE PROCEDURE mesh.session_set(
    _sid text
)
AS $$
    SELECT set_config('mess.sid', _sid, false);
$$ LANGUAGE sql;
