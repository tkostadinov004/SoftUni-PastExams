function horseRacing(params) {
    let horseArray = params[0].split("|");
    const commands = params.slice(1);
    for (const cmd of commands) {
        if(cmd == "Finish") {
            break;
        }

        const cmdSplitted = cmd.split(" ");
        switch (cmdSplitted[0]) {
            case "Retake":
                const overtaking = cmdSplitted[1];
                const overtaken = cmdSplitted[2];

                const overtakingIndex = horseArray.indexOf(overtaking);
                const overtakenIndex = horseArray.indexOf(overtaken);
                if(overtakingIndex < overtakenIndex) {
                    let swap = horseArray[overtakingIndex];
                    horseArray[overtakingIndex] = horseArray[overtakenIndex];
                    horseArray[overtakenIndex] = swap;

                    console.log(`${overtaking} retakes ${overtaken}.`);
                }
                break;
            case "Trouble":
                const troubleHorse = cmdSplitted[1];

                const troubleHorseIndex = horseArray.indexOf(troubleHorse);
                if(troubleHorseIndex > 0) {
                    let swap = horseArray[troubleHorseIndex - 1];
                    horseArray[troubleHorseIndex - 1] = horseArray[troubleHorseIndex];
                    horseArray[troubleHorseIndex] = swap;

                    console.log(`Trouble for ${troubleHorse} - drops one position.`);
                }
                break;
            case "Rage":
                const ragingHorse = cmdSplitted[1];

                const ragingHorseIndex = horseArray.indexOf(ragingHorse);

                horseArray.splice(ragingHorseIndex, 1);
                let step = ragingHorseIndex + 2 <= horseArray.length ? ragingHorseIndex + 2 : ragingHorseIndex + ((ragingHorseIndex + 2) - horseArray.length);
                horseArray.splice(step, 0, ragingHorse);
                console.log(`${ragingHorse} rages 2 positions ahead.`);
                break;
            case "Miracle":
                const miracleHorse = horseArray.shift();
                horseArray.push(miracleHorse);
                console.log(`What a miracle - ${miracleHorse} becomes first.`);
                break;
        }
    }
    console.log(horseArray.join("->"));
    console.log(`The winner is: ${horseArray.reverse()[0]}`);
}