CREATE VIEW mesh.mess_goals
AS SELECT
	'stan.body.water'::text AS goal,
    (mess_drink_goals.value * 100)::integer::text || '%'::text AS targets,
    'Выпить воды'::text AS notes,
	(SELECT content FROM mesh.mess WHERE id = 'stan.body.water') AS content
FROM
	mesh.mess_drink_goals
WHERE
	(mess_drink_goals.at = CURRENT_DATE)
	AND (mess_drink_goals.value < 0.9)

UNION ALL SELECT
	'gmg.invoice'::text AS goal,
    (
		((((100 * sum(mess_gmg_done_days.hours))::double precision / (((7000.00 / 40)::double precision * (t.current - t.start)) / (t.finish - t.start))))::integer)::text || '%'::text
	) AS targets,
	NULL::text AS notes,
    (SELECT content FROM mesh.mess WHERE id = 'gmg.invoice') AS content
FROM
	mesh.mess_gmg_done_days,
    (
		SELECT
			date_part('epoch'::text, date_trunc('month'::text, (CURRENT_DATE)::timestamp with time zone)) AS date_part,
			date_part('epoch'::text, date_trunc('month'::text, (CURRENT_DATE + '1 mon'::interval))) AS date_part,
			date_part('epoch'::text, now()) AS current
    ) t(start, finish, current)
WHERE
	(mess_gmg_done_days.month = to_char((CURRENT_DATE)::timestamp with time zone, 'IYYY-MM'::text))
GROUP BY
	t.start,
	t.finish,
	t.current

UNION ALL SELECT
	id::text AS goal,
	at::text AS targets,
    title AS notes,
    content AS content
FROM
	mesh.mess_deadline_list
WHERE
	(mess_deadline_list.at < ((now())::date + 1))

UNION ALL SELECT
	id::text AS goal,
	index::text AS targets,
	title AS notes,
    content AS content
FROM
	mesh.mess_rank_list

UNION ALL SELECT
	id::text AS goal,
	count::text AS targets,
	'deps'::text AS notes,
    content AS content
FROM
	mesh.mess_depends_list
;
