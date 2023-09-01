function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm gathering supplies to deliver to the various towns along the western coast. I help many farmers and merchants ship and acquire many goods."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end