function sprintReview(params) {
    var n = Number(params[0]);
    var sprintBoard = params.slice(1, n + 1);
    var commands = params.slice(n + 1);
    var tasks = {};

    sprintBoard.forEach(line => {
        const [assignee, taskId, title, status, estimatedPoints] = line.split(":");
        if(!tasks.hasOwnProperty(assignee)) {
            tasks[assignee] = {
                taskList: []
            };
        }
        tasks[assignee].taskList.push({
            taskId,
            title,
            status,
            estimatedPoints
        });
    });

    commands.forEach(command => {
        const splitted = command.split(":");
        const assignee = splitted[1];
       switch (splitted[0]) {
        case "Add New":
            const [taskId, title, status, estimatedPoints] = splitted.slice(2);
            if(tasks.hasOwnProperty(assignee)) {
                tasks[assignee].taskList.push({
                    taskId, title, status, estimatedPoints
                });
            }
            else {
                console.log(`Assignee ${assignee} does not exist on the board!`);
            }
            break;
        case "Change Status":
            const [editTaskId, newStatus] = splitted.slice(2);
            if(tasks.hasOwnProperty(assignee)) {
                if(Object.entries(tasks[assignee].taskList).map(i => i[1].taskId).includes(editTaskId, 0)) {
                    const changeIndex = tasks[assignee].taskList.map(i => i.taskId).indexOf(editTaskId);
                    tasks[assignee].taskList[changeIndex].status = newStatus;
                }
                else {
                    console.log(`Task with ID ${editTaskId} does not exist for ${assignee}!`);
                }
            }
            else {
                console.log(`Assignee ${assignee} does not exist on the board!`);
            }
            break;
        case "Remove Task":
            const index = splitted[2];
            if(tasks.hasOwnProperty(assignee)) {
                if(index < tasks[assignee].taskList.length) {
                    tasks[assignee].taskList.splice(index, 1);
                }
                else {
                    console.log("Index is out of range!");
                }
            }
            else {
                console.log(`Assignee ${assignee} does not exist on the board!`);
            }
            break;
       }
    });

    var taskLists = Object.entries(tasks).map(entry => entry[1].taskList).flat();
    var toDo = Number(taskLists.filter(i => i.status == "ToDo").map(i => Number(i.estimatedPoints)).reduce((sum, i) => sum + i, 0));
    var inProgress = Number(taskLists.filter(i => i.status == "In Progress").map(i => Number(i.estimatedPoints)).reduce((sum, i) => sum + i, 0));
    var codeReview = Number(taskLists.filter(i => i.status == "Code Review").map(i => Number(i.estimatedPoints)).reduce((sum, i) => sum + i, 0));
    var done = Number(taskLists.filter(i => i.status == "Done").map(i => Number(i.estimatedPoints)).reduce((sum, i) => sum + i, 0));
    
    console.log(`ToDo: ${toDo}pts`);
    console.log(`In Progress: ${inProgress}pts`);
    console.log(`Code Review: ${codeReview}pts`);
    console.log(`Done: ${done}pts`);

    console.log(done >= toDo + inProgress + codeReview ? "Sprint was successful!" : "Sprint was unsuccessful...");
}

sprintReview(   [
    '4',
    'Kiril:BOP-1213:Fix Typo:Done:1',
    'Peter:BOP-1214:New Products Page:In Progress:2',
    'Mariya:BOP-1215:Setup Routing:ToDo:8',
    'Georgi:BOP-1216:Add Business Card:Code Review:3',
    'Add New:Sam:BOP-1237:Testing Home Page:Done:3',
    'Change Status:Georgi:BOP-1216:Done',
    'Change Status:Will:BOP-1212:In Progress',
    'Remove Task:Georgi:3',
    'Change Status:Mariya:BOP-1215:Done',
]

)