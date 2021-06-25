create view mess_burpee_stats(at, calories, calories_per_minute, repeats, intensity, set, format) as
SELECT tabata.at,
       set.repeats::numeric * 1.68                                                                       AS calories,
       ((set.repeats::numeric * 1.67 * 60::numeric)::double precision /
        (set.length::real * tabata.work::double precision))::numeric(5, 2)                               AS calories_per_minute,
       set.repeats,
       (set.length::real * tabata.work::double precision / set.repeats::double precision)::numeric(5, 2) AS intensity,
       tabata.set,
       format('%s/%s'::text, tabata.work, tabata.rest)                                                   AS format
FROM mesh.mess,
     LATERAL jsonb_to_recordset(mess.data -> 'tabatas'::text) tabata(at timestamp with time zone, set integer[],
                                                                     work integer, rest integer),
     LATERAL ( SELECT sum(unnest.unnest) AS sum,
                      count(*)           AS count
               FROM unnest(tabata.set) unnest(unnest)) set(repeats, length)
WHERE mess.id = 'stan.workout.hiit.burpee'::ltree;

alter table mess_burpee_stats
    owner to postgres;

