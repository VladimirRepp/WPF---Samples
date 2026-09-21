using System.ComponentModel;

namespace Wpf_Counter.Models
{
    /// <summary>
    /// Модель - чистая бизнес-логика
    /// </summary>
    public class Counter
    {
        private int _number;
    
        public int Count
        {
            get => _number;
            set => _number = value;
        }
    
        public Counter()
        {
            _number = 0; 
        }
         
        public Counter(int number)
        {
            _number = number;
        }
    
        public void Increment() => _number++;
        public void Decrement() => _number--;
    
        public void ChangeTo(int number) => _number += number;
    }
}
