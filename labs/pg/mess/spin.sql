create table mess.spin (
    id bigserial not null,
    chain bigint null,
    author text not null,
    url text not null,
    content xml null,
    visited timestamp not null
);

alter table
    mess.spin
add constraint
    pk_mess_spin
primary key (
    id
);

alter table
    mess.spin
add constraint
    fk_mess_spin_chain
foreign key (
    chain
) references mess.spin (
    id
);

create index
    ix_mess_spin_url
on mess.spin (
    url
);

create index
    ix_mess_spin_chain
on mess.spin (
    chain
);
