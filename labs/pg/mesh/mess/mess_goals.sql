create view mess_goals(goal, targets, notes, content) as
SELECT 'stan.body.water'::text                                             AS goal,
       (mess_drink_goals.value * 100::numeric)::integer::text || '%'::text AS targets,
       'Выпить воды'::text                                                 AS notes,
       NULL::xml                                                           AS content
FROM mesh.mess_drink_goals
WHERE mess_drink_goals.at = CURRENT_DATE
  AND mess_drink_goals.value < 0.9
UNION ALL
SELECT 'gmg.invoice'::text AS goal,
       ((100::numeric * sum(mess_gmg_done_days.hours))::double precision /
        ((7000.00 / 40::numeric)::double precision * (t.current - t.start) / (t.finish - t.start)))::integer::text ||
       '%'::text           AS targets,
       NULL::text          AS notes,
       NULL::xml           AS content
FROM mesh.mess_gmg_done_days,
     (SELECT date_part('epoch'::text, date_trunc('month'::text, CURRENT_DATE::timestamp with time zone)) AS date_part,
             date_part('epoch'::text, date_trunc('month'::text, CURRENT_DATE + '1 mon'::interval))       AS date_part,
             date_part('epoch'::text, now())                                                             AS current) t(start, finish, current)
WHERE mess_gmg_done_days.month = to_char(CURRENT_DATE::timestamp with time zone, 'IYYY-MM'::text)
GROUP BY t.start, t.finish, t.current
UNION ALL
SELECT mess_deadline_list.id::text AS goal,
       mess_deadline_list.at::text AS targets,
       mess_deadline_list.title    AS notes,
       mess_deadline_list.content AS content
FROM mesh.mess_deadline_list
WHERE mess_deadline_list.at < (now()::date + 1)
UNION ALL
SELECT mess_rank_list.id::text    AS goal,
       mess_rank_list.index::text AS targets,
       mess_rank_list.title       AS notes,
       NULL::xml                  AS content
FROM mesh.mess_rank_list
UNION ALL
SELECT mess_depends_list.id::text    AS goal,
       mess_depends_list.count::text AS targets,
       'deps'::text                  AS notes,
       NULL::xml                     AS content
FROM mesh.mess_depends_list;

alter table mess_goals
    owner to postgres;

