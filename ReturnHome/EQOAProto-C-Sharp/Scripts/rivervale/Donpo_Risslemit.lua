function  event_say(choice)
diagOptions = {}
    npcDialogue = "This 'ere windmill keeps me tied up day in and day out. Darn things leaves no time for my errands."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end