-- auto-generated definition
create sequence id_sequence
    minvalue 0
    maxvalue 1048575
    cycle;

alter sequence id_sequence owner to postgres;

grant select, update, usage on sequence id_sequence to yavulan;

