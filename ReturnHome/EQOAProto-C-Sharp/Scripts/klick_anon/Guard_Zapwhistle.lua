function  event_say(choice)
diagOptions = {}
    npcDialogue = "Lying below Redspit Mountain is a valley known as Burnflow, named after the sheets of lava that once poured over the landscape. The valley is only accessible through three narrow passes, making the area easily defensible."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end