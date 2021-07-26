create table mess.spin (
    id bigserial not null,

    -- Next spin in the chain. If it is null, then it's the last spin for the user.
    spin bigint null,

    -- Temporary this is a user. Later can be profile for multi-profile users.
    profile text not null,

    url ltree not null,

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
    spin
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
    spin
);

create unique index
    ux_mess_spin_profile_spin
on mess.spin (
    profile,
    spin
);