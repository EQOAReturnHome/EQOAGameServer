function  event_say(choice)
diagOptions = {}
    npcDialogue = "Mind yer curses, this is the church. The bishop wont be pleased to hear you swearing up a storm in this corner of the city."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end