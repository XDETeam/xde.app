CREATE TRIGGER
    node_view_change_trigger
INSTEAD OF INSERT OR UPDATE OR DELETE ON
      mesh.node_view
FOR EACH ROW EXECUTE PROCEDURE
    mesh.node_view_change()
;
