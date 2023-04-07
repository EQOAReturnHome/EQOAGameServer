local function testing()
    print("I'm inside the testing function")
end

function event_say(choice)
    diagOptions = {}
    npcDialogue =
        "No it isn't a good day! Everyone here is simply trying to relieve me of my coin! You will keep your distance, before I call the guard!"
    testing()
    SendDialogue(mySession, npcDialogue, diagOptions)
end