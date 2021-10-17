CREATE PROCEDURE mesh.mess_burpee1(
	_set integer[],
	_work integer DEFAULT 20,
	_rest integer DEFAULT 10,
	_at timestamp without time zone DEFAULT timezone('UTC'::text, now())
)
LANGUAGE sql
AS
$$
	UPDATE
        mess.mess
    SET
        data = jsonb_insert(
            data,
            '{tabatas, 0}',
            jsonb_build_object(
                'set', _set,
                'work', _work,
                'rest', _rest,
                'at', _at
            )
        )
    WHERE
        id = 'stan.workout.hiit.burpee'
    ;
$$;
