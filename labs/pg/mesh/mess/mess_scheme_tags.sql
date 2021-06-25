create view mess_scheme_tags(name, node, id) as
SELECT tag.name,
       tag.node,
       mess.id
FROM mess.mess,
     LATERAL XMLTABLE(('//*'::text) PASSING (mess.content)
                      COLUMNS name text PATH ('name(.)'::text), node xml PATH ('.'::text)) tag;

alter table mess_scheme_tags
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_scheme_tags to yavulan;

