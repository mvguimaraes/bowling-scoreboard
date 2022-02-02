# Bowling scoreboard

- Logic is wrapped into BowlingScore.Service project
	- `ScoreboardService` class is the entrypoint
	- Frames and Rolls are represented by their own classes
	- As the 10th frame has its particular way to handle rolls, two handlers were created:
		- `RegularFrameHandler` 
		- `TenthFrameHandler`
- Unit tests were created
- There is a command line (awful, unstable) client to serve as playground


- **Things to enhance**:
	- avoid multiple lookup on the list of rolls
	- add a better validation logi for input params
	- reduce if nesting