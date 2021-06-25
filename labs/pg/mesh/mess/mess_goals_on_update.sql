create function mess_goals_on_update() returns trigger
    language plpgsql
as
$$
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
$$;

alter function mess_goals_on_update() owner to postgres;

