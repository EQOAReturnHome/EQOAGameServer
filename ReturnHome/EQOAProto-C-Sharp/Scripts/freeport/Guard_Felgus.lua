function  event_say(choice)
diagOptions = {}
    npcDialogue = "I need to get out of this job, especially after what happened to the last cadet. I can still hear his screams�I wonder if Qeynos is hiring�"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end