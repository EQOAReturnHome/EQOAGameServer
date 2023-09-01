function event_say()
diagOptions = {}
    npcDialogue = "There are still bands of elves fighting to stymie the efforts of the Teir'Dal back in our old home of Takish'Hiz. They are the Truearrow Warriors, and their noble efforts will be commended upon their return."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end