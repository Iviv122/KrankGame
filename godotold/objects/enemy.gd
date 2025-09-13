extends CharacterBody3D

@export var movement_speed: float = 4.0
@export var player: Node3D
@onready var navigation_agent: NavigationAgent3D = get_node("NavigationAgent3D")

func _ready() -> void:
	navigation_agent.velocity_computed.connect(Callable(_on_velocity_computed))
	if player:
		set_movement_target(player.position)
	else:
		push_error("Player not assigned!")

func set_movement_target(movement_target: Vector3):
	navigation_agent.set_target_position(movement_target)
	print("Target set:", movement_target)

func _physics_process(delta):
	if navigation_agent.is_navigation_finished():
		print("Path finished")
		if player:
			set_movement_target(player.position)
		return
	await get_tree().process_frame # Let it calculate the path
	var next_path_position: Vector3 = navigation_agent.get_next_path_position()
	var new_velocity: Vector3 = position.direction_to(next_path_position) * movement_speed

	if navigation_agent.avoidance_enabled:
		navigation_agent.set_velocity(new_velocity)
	else:
		_on_velocity_computed(new_velocity)

func _on_velocity_computed(safe_velocity: Vector3):
	print("Moving with velocity:", safe_velocity)
	velocity = safe_velocity
	move_and_slide()
