extends Camera3D

@export var sensX : float = 0.005
@export var sensY : float = 0.005

@export var tilt : float = 0

func _unhandled_input(event: InputEvent) -> void:
	if event is InputEventMouseMotion:
		if Input.mouse_mode == Input.MOUSE_MODE_CAPTURED:
			rotation.x -= event.relative.y * sensX 
			rotation.y -= event.relative.x * sensY
			rotation.x = clamp(rotation.x, deg_to_rad(-90), deg_to_rad(90))
			rotation.z = tilt


func _ready() -> void:
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED


func _process(delta: float) -> void:
	pass
