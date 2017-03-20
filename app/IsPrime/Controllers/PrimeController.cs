using Microsoft.AspNetCore.Mvc;

namespace IsPrime.Controllers
{
    [Route("api/[controller]")]

    public class PrimeController : Controller
    {
        [HttpGet("{number}")]
        public bool Get(int number)
        {
            return IsPrimeNumber(number);
        }

        private bool IsPrimeNumber(int candidate)
        {
            if ((candidate & 1) == 0)
            {
                if (candidate == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            for (int i = 3; (i * i) <= candidate; i += 2)
            {
                if ((candidate % i) == 0)
                {
                    return false;
                }
            }

            return candidate != 1;
        }
    }
}