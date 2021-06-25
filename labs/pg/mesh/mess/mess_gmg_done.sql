create view mess_gmg_done(at, time, task) as
SELECT done.at,
       done."time",
       done.task
FROM mess.mess,
     LATERAL XMLTABLE(('//done'::text) PASSING (mess.content)
                      COLUMNS at date PATH ('@at'::text), "time" integer PATH ('@time'::text), task text PATH ('text()'::text), issued date PATH ('../@issued'::text)) done
WHERE mess.id = 'gmg.invoice'::ltree
  AND done.issued IS NULL
ORDER BY done.at DESC;

alter table mess_gmg_done
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_gmg_done to yavulan;

