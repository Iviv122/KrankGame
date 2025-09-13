extends CharacterBody3D

@export var height = 2.1
@export var speed = 10.0
@export var jump_force = 4.5

@export var cam : Camera3D 


var wall_jump : bool = true 
var input_dir = Vector2(0, 0)
var direction : Vector3 
func _process(_delta):
	input_dir = Vector2(0, 0)

	if Input.is_action_pressed("forward"):
		input_dir.y -= 1;
	if Input.is_action_pressed("back"):
		input_dir.y += 1;
	if Input.is_action_pressed("left"):
		input_dir.x -= 1;
	if Input.is_action_pressed("right"):
		input_dir.x += 1;
	if  Input.is_action_pressed("jump") and _is_on_floor():
		velocity.y = jump_force
		wall_jump = true 
		print("Floor jump")
	elif Input.is_action_just_pressed("jump") and in_wall_dir() and wall_jump:
		velocity.y = jump_force
		wall_jump = false
		print("Wall jump")

func _is_on_floor():

	var space_state = get_world_3d().direct_space_state
	var direction = Vector3.DOWN*height/2 

	var start: Vector3 = global_position
	var end: Vector3 = start + direction
	var ray = PhysicsRayQueryParameters3D.create(start,end)
	var result = space_state.intersect_ray(ray) 
	
	return result

func in_wall_dir():

	var space_state = get_world_3d().direct_space_state

	var start: Vector3 = global_position
	var end: Vector3 = start + direction * 1.0

	var ray = PhysicsRayQueryParameters3D.create(start,end)
	var result = space_state.intersect_ray(ray) 
	return result

func _physics_process(delta: float) -> void:
	if _is_on_floor():
		wall_jump = true
	else:
		velocity += get_gravity() * delta

	direction = (cam.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()

	if direction:
		velocity.x = direction.x * speed
		velocity.z = direction.z * speed
	else:
		velocity.x = move_toward(velocity.x, 0, speed)
		velocity.z = move_toward(velocity.z, 0, speed)

	move_and_slide()
