create view mess_scales_stats(at, weight, fat, body_index) as
SELECT scales.at,
       scales.weight,
       scales.fat,
       scales.weight * scales.fat AS body_index
FROM mess.mess,
     LATERAL XMLTABLE(('//scales'::text) PASSING (mess.content)
                      COLUMNS at timestamp without time zone PATH ('@at'::text), weight numeric(4, 1) PATH ('@weight'::text), fat numeric(4, 1) PATH ('@fat'::text)) scales
WHERE mess.id = 'stan.gym.stats'::ltree;

alter table mess_scales_stats
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_scales_stats to yavulan;

