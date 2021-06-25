create view mess_rank_list(index, id, title, content) as
SELECT rank.index,
       mess.id,
       rank.title,
       mess.content
FROM mess.mess,
     LATERAL XMLTABLE(('//rank'::text) PASSING (mess.content)
                      COLUMNS index integer PATH ('@index'::text), title text PATH ('../@title'::text)) rank
ORDER BY rank.index;

alter table mess_rank_list
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_rank_list to yavulan;

