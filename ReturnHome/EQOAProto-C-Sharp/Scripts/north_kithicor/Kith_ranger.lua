function event_say()
diagOptions = {}
    npcDialogue = "Follow the road west and you will find Rivervale. If you dare travel east, be weary of the Gal 'Saris and the undead between us and Bobble-by-Water."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end