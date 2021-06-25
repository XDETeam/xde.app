create view mess_deadline_list(at, id, title, content) as
SELECT deadline.at,
       mess.id,
       deadline.title,
       mess.content
FROM mess.mess,
     LATERAL XMLTABLE(('//deadline'::text) PASSING (mess.content)
                      COLUMNS at timestamp without time zone PATH ('@at'::text), title text PATH ('../@title'::text)) deadline
ORDER BY deadline.at;

alter table mess_deadline_list
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_deadline_list to yavulan;

