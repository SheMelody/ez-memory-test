# EzMemoryTest
This tool allows you to quickly test your memory.

![screenshot2](https://github.com/SheMelody/ez-memory-test/assets/20774468/3ab6fa1b-dbbc-46be-b722-fb7bb240c371)

This is a process involving the following steps:
- Generating 1 GB of random data into your RAM
- Saving the data to disk
- Compressing the data into an archive
- Extracting the data off the archive
- Re-importing the data into memory
- Comparing the imported data with the previous data that is still stored into your RAM

You can optionally provide two positional arguments:
- The first argument tells the path in which make temporary files, it may otherwise create them inside the current working directory
- The second argument can be provided as any string to tell the program to not run in "interactive" mode, which means it won't hang waiting for user input

Either way, keep in mind that the program exits with code 1 (instead of 0) if the test fails.

### Don't use this as an extensive memory test. This test is designed to simply test whether this task survives, since errors in compression and decompression are frequent with bad RAM overclocks.

Our Discord server: http://discord.gg/6TMHU63
