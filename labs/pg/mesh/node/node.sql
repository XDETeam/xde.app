CREATE TABLE mesh.node (
    id mesh.id NOT NULL DEFAULT mesh.id_new(),
        CONSTRAINT pk_mesh_node PRIMARY KEY(id),

	owner mesh.id NULL,
		CONSTRAINT
			fk_mesh_node_owner
		FOREIGN KEY(
			owner
		) REFERENCES mesh.node (
			id
		),

	path text NOT NULL, --TODO:Temporary column
	content xml NOT NULL

	-- TODO: This fields may be a part of lifetime aspect.
    -- created timestamp NOT NULL,
    -- deleted timestamp NULL
) WITHOUT OIDS;
